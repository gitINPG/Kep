using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace KEP
{
    public partial class Form1 : Form

    {

        public Form1()

        {
            InitializeComponent();
        }
        //MonitoredItem writeMonitoredItem;
        MonitoredItem statusPLC;
        public Session m_session;
        private bool m_connectedOnce;
        private Subscription m_subscription;
        public  List<dataToLog> listDataToLog = new List<dataToLog>();
        public dTable table1 = new dTable();

        public string sqlConnectionString = "Server=LAPTOP-TLMS7TUU;database = MainDB;Trusted_Connection=True;Encrypt=False;user id=Szymon;password=Adminadminbwi1!";
        private string sqlTargetTable = "DatabaseKep";
        private string kepAddress = "opc.tcp://127.0.0.1:49320";
        private string kepServerLogin = "User_1";
        private string kepServerPassword = "Adminadminbwi1!";
        private string plcStatus = "_AdvancedTags.status";
        private string plcTrigger = "OPC.PLC.Trig.SimPLC__1__Trig";

        IDictionary<string, string> dataMapping = new Dictionary<string, string>(){
            {"OPC.PLC.IntValue.SimPLC__1__IntValue", "Value"},
            {"OPC.PLC.Name.SimPLC__1__Name", "Name"},
            {"_System._DateTimeLocal", "Datetime" }
         };

        IDictionary<string, string> dataMapping2 = new Dictionary<string, string>(){
            {"OPC.PLC2.Text.SimPLC2__1__Text", "Text" },
            {"OPC.PLC2.Bit.SimPLC2__1__Bit", "Bit"},
         };

        private void connectServerCtrl1_ConnectComplete(object sender, EventArgs e)

        {
            try
            {
                m_session = connectServerCtrl1.Session;

                if (m_session != null && !m_connectedOnce)
                {
                    m_connectedOnce = true;
                    CreateSubscriptionAndMonitorItem();
                    dbConnection db1 = new dbConnection(sqlTargetTable,plcStatus,plcTrigger,dataMapping,m_session,table1);
                    dbConnection db2 = new dbConnection("DatabaseKep2", "OPC.PLC2.Status.SimPLC2__1__Status", "OPC.PLC2.Trig.SimPLC2__1__Trig", dataMapping2, m_session, table1);
                    dataGridView1.DataSource = table1.table;
                }
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        private void CreateSubscriptionAndMonitorItem()
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }
                if (m_subscription != null)
                {
                    m_session.RemoveSubscription(m_subscription);
                    m_subscription = null;
                }

                //m_subscription = new Subscription();
                //m_subscription.PublishingEnabled = true;
                //m_subscription.PublishingInterval = 1000;
                //m_subscription.Priority = 1;
                //m_subscription.KeepAliveCount = 10;
                //m_subscription.LifetimeCount = 20;
                //m_subscription.MaxNotificationsPerPublish = 1000;
                //m_session.AddSubscription(m_subscription);
                //m_subscription.Create();

                //MonitoredItem monitoredStatusPLC = new MonitoredItem();
                //monitoredStatusPLC.StartNodeId = new NodeId(plcStatus, 2);
                //monitoredStatusPLC.AttributeId = Attributes.Value;
                //m_subscription.AddItem(monitoredStatusPLC);
                //m_subscription.ApplyChanges();
                //statusPLC = monitoredStatusPLC;
                //foreach (var dataMap in dataMapping)
                //{
                //    dataToLog data = new dataToLog(dataMap.Key, dataMap.Value,sqlTargetTable);
                //    MonitoredItem monitoredData = new MonitoredItem();
                //    monitoredData.StartNodeId = new NodeId(dataMap.Key, 2);
                //    monitoredData.AttributeId = Attributes.Value;
                //    monitoredData.Notification += MonitoredData_Notification;
                //    listDataToLog.Add(data);
                //    table1.addRecord(data);
                //    m_subscription.AddItem(monitoredData);
                //    m_subscription.ApplyChanges();
                //}
                //MonitoredItem monitoredTrig = new MonitoredItem();
                //monitoredTrig.StartNodeId = new NodeId(plcTrigger, 2);
                //monitoredTrig.AttributeId = Attributes.Value;
                //monitoredTrig.Notification += MonitoredItem_Notification;
                //m_subscription.AddItem(monitoredTrig);
                //m_subscription.ApplyChanges();

                //MonitoredItem itemToWrite = new MonitoredItem();
                //itemToWrite.StartNodeId = new NodeId("AB.CompactLogix.Prgm_MainProgram.ReceivedData", 2);
                //itemToWrite.AttributeId = Attributes.Value;
                //itemToWrite.Notification += MonitoredItem_Notification;
                //m_subscription.AddItem(itemToWrite);
                //m_subscription.ApplyChanges();
                //writeMonitoredItem = itemToWrite;

            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }

        }
        //private void MonitoredData_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new MonitoredItemNotificationEventHandler(MonitoredData_Notification), monitoredItem, e);
        //        return;
        //    }
        //    try
        //    {
        //        if ((bool)(((MonitoredItemNotification)statusPLC.LastValue).Value.WrappedValue.Value) == false)
        //        {
        //            textBoxStatusAB.Text = "Connected";
        //            foreach (dataToLog data in listDataToLog)
        //            {
        //                if (monitoredItem.ResolvedNodeId.Identifier.ToString() == data.nodeId)
        //                {
        //                    data.value = ((MonitoredItemNotification)e.NotificationValue).Value.WrappedValue.Value;
        //                    data.dataType = monitoredItem.LastValue.GetType().ToString();
        //                    table1.editRecord(data);
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            textBoxStatusAB.Text = "Communication Error";
        //            textBoxTrig.Text = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientUtils.HandleException(this.Text, ex);
        //    }
        //}
        //private void MonitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new MonitoredItemNotificationEventHandler(MonitoredItem_Notification), monitoredItem, e);
        //        return;
        //    }
        //    try
        //    {
        //        if ((bool)(((MonitoredItemNotification)statusPLC.LastValue).Value.WrappedValue.Value) == false)
        //        {
        //            textBoxStatusAB.Text = "Connected";

        //            if (monitoredItem.ResolvedNodeId.Identifier.ToString() == plcTrigger)
        //            {
        //                textBoxTrig.Text = ((MonitoredItemNotification)e.NotificationValue).Value.ToString();
        //                if (((MonitoredItemNotification)e.NotificationValue).Value.ToString() == "True")
        //                {
        //                    try
        //                    {
        //                        string sqlQuery = "Insert into " + sqlTargetTable + " (";
        //                        foreach (dataToLog data in listDataToLog)
        //                        {
        //                            sqlQuery += data.columnName + ",";
        //                        }
        //                        sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);
        //                        sqlQuery += ") ";
        //                        sqlQuery += "Values (";
        //                        foreach (dataToLog data in listDataToLog)
        //                        {
        //                            sqlQuery += "@" + data.columnName + ",";
        //                        }
        //                        sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);
        //                        sqlQuery += ");";

        //                        using (SqlConnection conn = new SqlConnection(sqlConnectionString))
        //                        {
        //                            conn.Open();
        //                            using (SqlCommand cmd1 = new SqlCommand(sqlQuery, conn))
        //                            {
        //                                foreach (dataToLog data in listDataToLog)
        //                                {
        //                                    cmd1.Parameters.AddWithValue("@" + data.columnName, data.value);
        //                                }
        //                                cmd1.ExecuteNonQuery();
        //                            }
        //                            conn.Close();
        //                        }
        //                    }
        //                    catch (SqlException ex)
        //                    {
        //                        string msg = "SQL Close Error:";
        //                        msg += ex.Message;
        //                        MessageBox.Show(msg, "Close Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            textBoxStatusAB.Text = "Communication Error";
        //            textBoxTrig.Text = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientUtils.HandleException(this.Text, ex);
        //    }
        //}

        private System.Windows.Forms.Timer x = new System.Windows.Forms.Timer();
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckConnection(sender, e);
            x.Interval = (20000);
            x.Tick += new EventHandler(CheckConnection);
            x.Start();

            this.connectServerCtrl1.ServerUrl = kepAddress;
            string AppName = "OpcKepServer";
            ApplicationConfiguration config = new ApplicationConfiguration()
            {
                ApplicationName = AppName,
                ApplicationUri = Utils.Format(@"urn:{0}:" + AppName, System.Net.Dns.GetHostName()),
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier
                    {
                        StoreType = @"Directory",
                        StorePath = System.Windows.Forms.Application.StartupPath + @"\Cert\TrustedIssuer",
                        SubjectName = "CN=" + AppName + ", DC=" + System.Net.Dns.GetHostName()
                    },
                    TrustedIssuerCertificates = new CertificateTrustList
                    {
                        StoreType = @"Directory",
                        StorePath = System.Windows.Forms.Application.StartupPath + @"\Cert\TrustedIssuer"
                    },
                    TrustedPeerCertificates = new CertificateTrustList
                    {
                        StoreType = @"Directory",
                        StorePath = System.Windows.Forms.Application.StartupPath + @"\Cert\TrustedIssuer"
                    },
                    RejectedCertificateStore = new CertificateTrustList
                    {
                        StoreType = @"Directory",
                        StorePath = System.Windows.Forms.Application.StartupPath + @"\Cert\RejectedCertificates"
                    },
                    AutoAcceptUntrustedCertificates = true,
                    AddAppCertToTrustedStore = true,
                    RejectSHA1SignedCertificates = false
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                TraceConfiguration = new TraceConfiguration
                {
                    DeleteOnLoad = true
                },
                DisableHiResClock = false
            };
            config.Validate(ApplicationType.Client).GetAwaiter().GetResult();
            if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                config.CertificateValidator.CertificateValidation += (s, ee) =>
                { ee.Accept = (ee.Error.StatusCode == StatusCodes.BadCertificateUntrusted); };
            }
            this.connectServerCtrl1.Configuration = config;
            this.connectServerCtrl1.UserIdentity = new UserIdentity(kepServerLogin, kepServerPassword);
            this.connectServerCtrl1.UseSecurity = true;
            var application = new ApplicationInstance

            {
                ApplicationName = AppName,
                ApplicationType = ApplicationType.Client,
                ApplicationConfiguration = config
            };
            //set 0 Trace mask => stop show log in output window
            Opc.Ua.Utils.SetTraceMask(0);
            application.CheckApplicationInstanceCertificate(true, 2048).GetAwaiter().GetResult(); //Create Certificate
        }

        private void checkBoxFromApp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (writeMonitoredItem != null)
                //{
                //    this.WriteTag(this.connectServerCtrl1.Session, this.writeMonitoredItem, checkBoxFromApp.Checked);
                //}
            }
            catch (SqlException ex)
            {
                string msg = "Close Error:";
                msg += ex.Message;
                MessageBox.Show(msg, "Close Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool WriteTag(Session m_session, MonitoredItem tag, object v)
        {
            Opc.Ua.WriteValue valueToWrite = new Opc.Ua.WriteValue();
            valueToWrite.AttributeId = Attributes.Value;
            string sType = tag.GetType().ToString();
            string tagID = tag.ResolvedNodeId.Identifier.ToString();
            return WriteTag(m_session, tagID, sType, v);
        }
        bool WriteTag(Session m_session, string tag, string sType, object v)
        {
            Opc.Ua.WriteValue valueToWrite = new Opc.Ua.WriteValue();
            valueToWrite.AttributeId = Attributes.Value;
            valueToWrite.NodeId = new NodeId(tag, 2);
            valueToWrite.Value.Value = GetValue(v, sType);
            valueToWrite.Value.ServerTimestamp = DateTime.MinValue;
            valueToWrite.Value.StatusCode = StatusCodes.Good;

            WriteValueCollection lstToWrite = new WriteValueCollection();
            lstToWrite.Add(valueToWrite);

            StatusCodeCollection results = null;
            DiagnosticInfoCollection lstDia = null;
            m_session.Write(null, lstToWrite, out results, out lstDia);
            ClientBase.ValidateResponse(results, lstToWrite);
            if (StatusCode.IsBad(results[0]))
            {
                return false;
            }
            return true;
        }
        private object GetValue(object v, string sType)
        {
            switch (sType)
            {
                case "Boolean":
                    return Convert.ToBoolean(v);
                case "Byte":
                    return Convert.ToByte(v);
                case "SByte":
                    return Convert.ToSByte(v);
                case "UInt16":
                    return Convert.ToUInt16(v);
                case "Int16":
                    return Convert.ToInt16(v);
                case "UInt32":
                    return Convert.ToUInt32(v);
                case "Int32":
                    return Convert.ToInt32(v);
                case "UInt64":
                    return Convert.ToUInt64(v);
                case "Int64":
                    return Convert.ToInt64(v);
                case "Double":
                    return Convert.ToDouble(v);
                case "Float":
                    return Convert.ToDateTime(v);
                case "DateTime":
                    return Convert.ToDateTime(v);
            }
            return v;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.connectServerCtrl1.Disconnect();
        }

        private void CheckConnection(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(sqlConnectionString);
                conn.Open();
                textBoxSqlCon.Text = "Connected";
            }
            catch (SqlException ex)
            {
                textBoxSqlCon.Text = "Disconnected";
                string msg = "SQL Close Error:";
                msg += ex.Message;
                MessageBox.Show(msg, "Close Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            table1.table.DefaultView.RowFilter = string.Format("Column LIKE '%{0}%'", textBoxFilter.Text);      
        }
    }
}
