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

        private Session m_session;
        private bool m_connectedOnce;
        private Subscription m_subscription;
        private IEncodeable val;

        private void connectServerCtrl1_ConnectComplete(object sender, EventArgs e)
        {
            try
            {
                m_session = connectServerCtrl1.Session;

                if (m_session != null && !m_connectedOnce)
                {
                    m_connectedOnce = true;
                    CreateSubscriptionAndMonitorItem();
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

                m_subscription = new Subscription();
                m_subscription.PublishingEnabled = true;
                m_subscription.PublishingInterval = 1000;
                m_subscription.Priority = 1;
                m_subscription.KeepAliveCount = 10;
                m_subscription.LifetimeCount = 20;
                m_subscription.MaxNotificationsPerPublish = 1000;

                m_session.AddSubscription(m_subscription);
                m_subscription.Create();


                MonitoredItem monitoredTrig = new MonitoredItem();
                monitoredTrig.StartNodeId = new NodeId("OPC.PLC.Trig.SimPLC__1__Trig", 2);
                monitoredTrig.AttributeId = Attributes.Value;
                monitoredTrig.Notification += MonitoredItem_Notification;
                m_subscription.AddItem(monitoredTrig);

                MonitoredItem monitoredName = new MonitoredItem();
                monitoredName.StartNodeId = new NodeId("OPC.PLC.Name.SimPLC__1__Name", 2);
                monitoredName.AttributeId = Attributes.Value;
                monitoredName.Notification += MonitoredItem_Notification;
                m_subscription.AddItem(monitoredName);

                MonitoredItem monitoredId = new MonitoredItem();
                monitoredId.StartNodeId = new NodeId("OPC.PLC.Id.SimPLC__1__Id", 2);
                monitoredId.AttributeId = Attributes.Value;
                monitoredId.Notification += MonitoredItem_Notification;
                m_subscription.AddItem(monitoredId);

                MonitoredItem monitoredVal = new MonitoredItem();
                monitoredVal.StartNodeId = new NodeId("OPC.PLC.IntValue.SimPLC__1__IntValue", 2);
                monitoredVal.AttributeId = Attributes.Value;
                monitoredVal.Notification += MonitoredItem_Notification;
                m_subscription.AddItem(monitoredVal);

                m_subscription.ApplyChanges();
                
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
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
                if (monitoredItem.StartNodeId.Identifier.ToString() == "OPC.PLC.Id.SimPLC__1__Id")
                {
                    textBoxID.Text = ((MonitoredItemNotification)e.NotificationValue).Value.ToString();
                }
                else if (monitoredItem.StartNodeId.Identifier.ToString() == "OPC.PLC.IntValue.SimPLC__1__IntValue")
                {
                    textBoxValue.Text = ((MonitoredItemNotification)e.NotificationValue).Value.ToString();
                }
                
                
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException(this.Text, ex);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.connectServerCtrl1.ServerUrl = "opc.tcp://127.0.0.1:49320";
            string AppName = "Kep";
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
            this.connectServerCtrl1.UserIdentity = new UserIdentity();
            this.connectServerCtrl1.UseSecurity = true;

            var application = new ApplicationInstance
            {
                ApplicationName = AppName,
                ApplicationType = ApplicationType.Client,
                ApplicationConfiguration = config

            };
            
            Opc.Ua.Utils.SetTraceMask(0);//
            application.CheckApplicationInstanceCertificate(true, 2048).GetAwaiter().GetResult();//create certificate

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.connectServerCtrl1.Disconnect();
        }
    }
}
