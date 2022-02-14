using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Reflection;


namespace KEP
{
    public class dbConnection : Form1
    {
        private string sqlTargetTable;
        private string plcStatus;
        private string plcTrigger;
        public IDictionary<string, string> dataMapping;
        public Session m_session;
        public Subscription subscription;
        public MonitoredItem status;
        public List<MonitoredItem> monitoredItems;
        public List<dataToLog> listDataToLog = new List<dataToLog>();
        public dTable table1;

        public dbConnection(string sqlTargetTable, string plcStatus, string plcTrigger, IDictionary<string, string> dataMapping, Session session,dTable table)
        {
            this.sqlTargetTable = sqlTargetTable;
            this.plcStatus = plcStatus;
            this.plcTrigger = plcTrigger;
            this.dataMapping = dataMapping;
            this.m_session = session;
            this.table1 = table;

            subscription = new Subscription();
            subscription.PublishingEnabled = true;
            subscription.PublishingInterval = 1000;
            subscription.Priority = 1;
            subscription.KeepAliveCount = 10;
            subscription.LifetimeCount = 20;
            subscription.MaxNotificationsPerPublish = 1000;
            m_session.AddSubscription(subscription);
            subscription.Create();

            MonitoredItem monitoredStatusPLC = new MonitoredItem();
            monitoredStatusPLC.StartNodeId = new NodeId(plcStatus, 2);
            monitoredStatusPLC.AttributeId = Attributes.Value;
            subscription.AddItem(monitoredStatusPLC);
            subscription.ApplyChanges();
            status = monitoredStatusPLC;
            foreach (var dataMap in dataMapping)
            {
                dataToLog data = new dataToLog(dataMap.Key, dataMap.Value, sqlTargetTable);
                MonitoredItem monitoredData = new MonitoredItem();
                monitoredData.StartNodeId = new NodeId(dataMap.Key, 2);
                monitoredData.AttributeId = Attributes.Value;
                monitoredData.Notification += MonitoredData_Notification;
                listDataToLog.Add(data);
                table1.addRecord(data);
                subscription.AddItem(monitoredData);
                subscription.ApplyChanges();
            }
            MonitoredItem monitoredTrig = new MonitoredItem();
            monitoredTrig.StartNodeId = new NodeId(plcTrigger, 2);
            monitoredTrig.AttributeId = Attributes.Value;
            monitoredTrig.Notification += MonitoredItem_Notification;
            subscription.AddItem(monitoredTrig);
            subscription.ApplyChanges();
        }

        private void MonitoredData_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MonitoredItemNotificationEventHandler(MonitoredData_Notification), monitoredItem, e);
                return;
            }
            try
            {
                if ((bool)(((MonitoredItemNotification)status.LastValue).Value.WrappedValue.Value) == false)
                {
                    //textBoxStatusAB.Text = "Connected";
                    foreach (dataToLog data in listDataToLog)
                    {
                        if (monitoredItem.ResolvedNodeId.Identifier.ToString() == data.nodeId)
                        {
                            data.value = ((MonitoredItemNotification)e.NotificationValue).Value.WrappedValue.Value;
                            data.dataType = monitoredItem.LastValue.GetType().ToString();
                            table1.editRecord(data);
                            break;
                        }
                    }
                }
                else
                {
               //     textBoxStatusAB.Text = "Communication Error";
               //     textBoxTrig.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException(this.Text, ex);
            }
        }
        private void MonitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MonitoredItemNotificationEventHandler(MonitoredItem_Notification), monitoredItem, e);
                return;
            }
            try
            {
                if ((bool)(((MonitoredItemNotification)status.LastValue).Value.WrappedValue.Value) == false)
                {
                    //textBoxStatusAB.Text = "Connected";

                    if (monitoredItem.ResolvedNodeId.Identifier.ToString() == plcTrigger)
                    {
                        //textBoxTrig.Text = ((MonitoredItemNotification)e.NotificationValue).Value.ToString();
                        if (((MonitoredItemNotification)e.NotificationValue).Value.ToString() == "True")
                        {
                            try
                            {
                                string sqlQuery = "Insert into " + sqlTargetTable + " (";
                                foreach (dataToLog data in listDataToLog)
                                {
                                    sqlQuery += data.columnName + ",";
                                }
                                sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);
                                sqlQuery += ") ";
                                sqlQuery += "Values (";
                                foreach (dataToLog data in listDataToLog)
                                {
                                    sqlQuery += "@" + data.columnName + ",";
                                }
                                sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);
                                sqlQuery += ");";

                                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = new SqlCommand(sqlQuery, conn))
                                    {
                                        foreach (dataToLog data in listDataToLog)
                                        {
                                            cmd1.Parameters.AddWithValue("@" + data.columnName, data.value);
                                        }
                                        cmd1.ExecuteNonQuery();
                                    }
                                    conn.Close();
                                }
                            }
                            catch (SqlException ex)
                            {
                                string msg = "SQL Close Error:";
                                msg += ex.Message;
                                MessageBox.Show(msg, "Close Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    //textBoxStatusAB.Text = "Communication Error";
                    //textBoxTrig.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException(this.Text, ex);
            }
        }

        
    }
}
