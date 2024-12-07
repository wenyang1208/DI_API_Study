using SAPbobsCOM;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public static SAPbobsCOM.Company oCompany;
        string MySalesOrder;
        string MySalesInvoice;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                oCompany = new SAPbobsCOM.Company();
                oCompany.Server = "DESKTOP-TO7FS9O";
                oCompany.SLDServer = "DESKTOP-TO7FS9O:40000";
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                oCompany.CompanyDB = "SBODemoGB";
                oCompany.UserName = "manager";
                oCompany.Password = "1234";
                oCompany.DbUserName = "sa";
                oCompany.DbPassword = "Wenyang1208#";
                int ret = oCompany.Connect();
                if (ret == 0)
                    MessageBox.Show("Connection ok");
                else
                    MessageBox.Show("Connection failed: " + oCompany.GetLastErrorCode().ToString()
                    + " - " + oCompany.GetLastErrorDescription());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (oCompany.Connected == true)
                {
                    oCompany.Disconnect();
                    MessageBox.Show("You are now disconnected");
                }
                else MessageBox.Show("You are not connected to the company.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            // independent from try/catch, the process must run the code in 'finally'
            finally
            {
                // reduce the reference count to 0, reference is the variable/pointer which holds the COM object.
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany);
                oCompany = null;
                Application.Exit();
            }

        }
        // creating sales order
        private void button3_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.Documents oSO;
            oSO = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
            try
            {
                oSO.CardCode = "C20000";
                oSO.DocDueDate = DateTime.Today;
                oSO.Comments = "this is my sales order’s comment";
                oSO.Lines.ItemCode = "A00001";
                oSO.Lines.Quantity = 2;
                oSO.Lines.Price = 100;
                oSO.Lines.Add();
                oSO.Lines.ItemCode = "A00002";
                oSO.Lines.Quantity = 1;
                oSO.Lines.Price = 50;
                // Add() : Method of the Documents object
                // (in your case, the Sales Order, oSO) that saves the changes to the database
                int ret = oSO.Add();
                if (ret == 0)
                {
                    // out: pass a parameter by reference and to allow the method to return a value
                    // through that parameter.
                    oCompany.GetNewObjectCode(out MySalesOrder);
                    MessageBox.Show("Add Sales Order successful - " + MySalesOrder);
                }
                else
                {
                    MessageBox.Show("Add Sales Order failed: " +
                    oCompany.GetLastErrorDescription());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oSO);
                oSO = null;
            }
        }

        // creating a/r invoice based on sales order
        private void invoice_button_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.Documents oSO;
            SAPbobsCOM.Documents oInv;

            oSO = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
            oInv = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

            try
            {
                // get the sales order key
                oSO.GetByKey(Int32.Parse(MySalesOrder));
                oInv.CardCode = oSO.CardCode;

                // add oSO data into each line of the invoice
                for (int i = 0; i <= oSO.Lines.Count - 1; i++)
                {
                    oInv.Lines.BaseEntry = oSO.DocEntry;
                    oInv.Lines.BaseLine = i;
                    oInv.Lines.BaseType = 17; // object type of sales order
                    oInv.Lines.Add();
                }

                int ret = oInv.Add();
                if (ret == 0)
                {

                    oCompany.GetNewObjectCode(out MySalesInvoice);
                    MessageBox.Show("Add Invoice successfully");

                }
                else
                    MessageBox.Show("Add Invoice failed: " + oCompany.GetLastErrorDescription());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oSO);
                oSO = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oInv);
                oInv = null;
            }
        }

        private void payment_button_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.Payments oPay = (SAPbobsCOM.Payments)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
            SAPbobsCOM.Documents oInv = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

            try
            {
                // refer the sales invoice key 
                oInv.GetByKey(Int32.Parse(MySalesInvoice));

                // assign customer code on payment based on a/r invoice
                oPay.CardCode = oInv.CardCode;
                oPay.Invoices.DocEntry = oInv.DocEntry;
                oPay.CashAccount = "112000";
                oPay.CashSum = oInv.DocTotal;

                // add and save documents
                int ret = oPay.Add();
                if (ret == 0)
                {
                    MessageBox.Show("Add payment successfully.");
                }
                else
                {
                    MessageBox.Show("Add Payment failed: " + oCompany.GetLastErrorDescription());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oInv);
                oInv = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oPay);
                oPay = null;
            }
        }

        // save invoice into xml.file
        private void workingwithxml_Click(object sender, EventArgs e)
        {
            try
            {
                // Export to XML only valid fields that support
                // XML import(read/ write fields only) from the
                // database.
                oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ExportImportMode;
                SAPbobsCOM.Documents oInv;

                oInv = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

                if (oInv.GetByKey(Int32.Parse(MySalesInvoice)))
                {
                    oInv.SaveXML("C:\\Users\\wenya\\Documents\\work\\invoice\\invoice_" + MySalesInvoice + ".xml");
                    MessageBox.Show("Invoice" + MySalesInvoice + " exported to XML");
                }
                else
                {
                    MessageBox.Show("Get Invoice failed: " + MySalesInvoice + " - " + oCompany.GetLastErrorDescription());
                }

                // Retrieves the XML schema used to define the structure and content of the object.
                //string schema;
                //schema = oCompany.GetBusinessObjectXmlSchema(SAPbobsCOM.BoObjectTypes.oInvoices);
                //MessageBox.Show(schema);

                oInv = oCompany.GetBusinessObjectFromXML("C:\\Users\\wenya\\Documents\\work\\invoice\\invoice_" + MySalesInvoice + ".xml", 0);
                oInv.Comments = "invoice loaded from xml";
                int ret = oInv.Add();
                if (ret == 0)
                {
                    oCompany.GetNewObjectCode(out MySalesInvoice);
                    MessageBox.Show("Invoice " + MySalesInvoice + " added from xml.");
                }
                else
                    MessageBox.Show("Adding invoice " + MySalesInvoice + " from XML failed: " + oCompany.GetLastErrorDescription());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
            }
        }

        private void Transaction_Click(object sender, EventArgs e)
        {
            if (!oCompany.InTransaction)
            {
                oCompany.StartTransaction();
            }

            // UPDATE TWO ITEM MASTER DATA RECORDS IN THE TRANSACTION
            SAPbobsCOM.Items oItem = (SAPbobsCOM.Items)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
            oItem.GetByKey("A00001");
            oItem.ValidRemarks = "DI test - " + DateTime.Now.ToString("h:mm:ss tt");
            int ret1 = oItem.Update();
            string res1 = oItem.ItemCode + " :" + oCompany.GetLastErrorDescription();

            oItem.GetByKey("A00002");
            oItem.ValidRemarks = "DI test - " + DateTime.Now.ToString("h:mm:ss tt");
            int ret2 = oItem.Update();
            string res2 = oItem.ItemCode + " :" + oCompany.GetLastErrorDescription();

            // CLOSE THE TRANSACTION (END TRANSACTION OF THE COMPANY OBJECT)
            // if successful, commit
            if (ret1 == 0 & ret2 == 0)
            {
                MessageBox.Show("item update successful");
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
                else
                {
                    throw new Exception("ERROR: Transaction closed before EndTransaction!!!");
                }
            }
            // otherwise, rollback
            else
            {
                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                }
                if (res1.Length > 1)
                {
                    MessageBox.Show("item update failed: " + res1);
                }
                if (res2.Length > 1)
                {
                    MessageBox.Show("item update failed: " + res2);
                }
            }
            MessageBox.Show("Intransaction state = " + oCompany.InTransaction.ToString());
        }

        private void genObj_Click(object sender, EventArgs e)
        {
            GeneralObjects bpForm = new GeneralObjects();
            bpForm.Show();
        }

        // Add udf into the OITM table in SAP
        private void udf_Click(object sender, EventArgs e)
        {
            try
            {
                SAPbobsCOM.UserFieldsMD oUDF;
                oUDF = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                oUDF.TableName = "OITM";
                oUDF.Name = "TB1_Course";
                oUDF.Description = "Course UDF";
                oUDF.Type = SAPbobsCOM.BoFieldTypes.db_Alpha;
                oUDF.EditSize = 20; // Defines how many characters the user can input into the field via the UI.

                int ret = oUDF.Add();
                if (ret == 0)
                {
                    MessageBox.Show("The field " + oUDF.Name + " is added.");
                }
                else
                {
                    MessageBox.Show("Error in adding " + oUDF.Name + " field, " + oCompany.GetLastErrorDescription());
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUDF);
                // Garbage Collector
                GC.Collect();
            }
            catch { }
        }

        // Create UDF for tables
        private void create_field(string MyTableName, string MyFieldName, string MyFieldDescription, SAPbobsCOM.BoFieldTypes MyFieldType, int MyEditSize)
        {
            try
            {
                SAPbobsCOM.UserFieldsMD oUDF;
                oUDF = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

                oUDF.TableName = MyTableName;
                oUDF.Name = MyFieldName;
                oUDF.Description = MyFieldDescription;
                oUDF.Type = MyFieldType;
                oUDF.EditSize = MyEditSize;

                // before Add(), if after ADD(), the default value of ValidValues might be assumed
                // in checkbox form
                if (oUDF.Name == "RENTED")
                {
                    oUDF.ValidValues.Value = "Y";
                    oUDF.ValidValues.Description = "Yes";
                    oUDF.ValidValues.Add();
                    oUDF.ValidValues.Value = "N";
                    oUDF.ValidValues.Description = "No";
                    oUDF.ValidValues.Add();
                }

                int ret = oUDF.Add();

                if (ret == 0)
                {

                    MessageBox.Show("Add field " + oUDF.Name + " sucessfully.");
                }
                else
                {
                    MessageBox.Show("Add field error " + oCompany.GetLastErrorDescription());
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject((oUDF));
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        // create multiple udf, more convenient maybe
        private void generate_fields(string MyTableName)
        {
            create_field(MyTableName, "AISLE", "Aisle Number", SAPbobsCOM.BoFieldTypes.db_Numeric, 2);
            create_field(MyTableName, "SECTION", "Section Number", SAPbobsCOM.BoFieldTypes.db_Alpha, 20);
            create_field(MyTableName, "RENTED", "Rented/Available", SAPbobsCOM.BoFieldTypes.db_Alpha, 1);
            create_field(MyTableName, "CARDCODE", "Card Code", SAPbobsCOM.BoFieldTypes.db_Alpha, 20);
        }

        // Add UDT
        private void add_udt_Click(object sender, EventArgs e)
        {
            try
            {
                SAPbobsCOM.UserTablesMD oUDT;
                string MyTableName;
                oUDT = (SAPbobsCOM.UserTablesMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                MyTableName = "TB1_VIDS";
                if (oUDT.GetByKey(MyTableName) == false)
                {
                    oUDT.TableName = MyTableName;
                    oUDT.TableDescription = "Video Management";
                    int ret = oUDT.Add();
                    if (ret == 0)
                    {
                        // also add UDF into the UDT
                        MessageBox.Show("Add table: " + oUDT.TableName);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUDT);
                        GC.Collect();
                        generate_fields(MyTableName);
                    }
                    else
                    {
                        MessageBox.Show("Add table error: " + oCompany.GetLastErrorDescription());
                    }
                }
                else
                {
                    MessageBox.Show("Table " + MyTableName + " already exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        // remove UDT
        private void del_udt_Click(object sender, EventArgs e)
        {
            try
            {
                SAPbobsCOM.UserTablesMD oUDT;
                string MyTableName;
                oUDT = (SAPbobsCOM.UserTablesMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                MyTableName = "TB1_VIDS";
                if (oUDT.GetByKey(MyTableName) == true)
                {
                    int ret = oUDT.Remove();
                    if (ret == 0)
                    {
                        MessageBox.Show("Remove table: " + oUDT.TableName + " sucessfully.");
                    }
                    else
                    {
                        MessageBox.Show("Remove table error: " + oCompany.GetLastErrorDescription());
                    }
                }
                else
                {
                    MessageBox.Show("Table: " + MyTableName + " not exists.");
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUDT);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }


        // generic function of the click button of add udt
        private void insert_record_UDT(string code, string name, int aisle, string section, string rented)
        {
            // Use UserTablesMD when you want to define or modify a user-defined table (its structure, name, description, etc.).
            // Use UserTable when you want to manipulate data in a user-defined table after it has been created.
            try
            {
                SAPbobsCOM.UserTable oUDT;

                oUDT = oCompany.UserTables.Item("TB1_VIDS");

                oUDT.Code = code;
                oUDT.Name = name;
                oUDT.UserFields.Fields.Item("U_AISLE").Value = aisle;
                oUDT.UserFields.Fields.Item("U_SECTION").Value = section;
                oUDT.UserFields.Fields.Item("U_RENTED").Value = rented;

                int ret = oUDT.Add();

                if (ret == 0)
                {
                    MessageBox.Show("Add Record: " + oUDT.Code + " successfully.");
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUDT);
                    GC.Collect();
                }
                else
                {
                    MessageBox.Show("Add Record error: " + oCompany.GetLastErrorDescription());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
        // insert new UDT records
        private void add_udt_rec_Click(object sender, EventArgs e)
        {
            insert_record_UDT("01", "Avatar", 15, "Sci-Fi", "Y");
            insert_record_UDT("02", "It", 2, "Thriller", "Y");
            insert_record_UDT("03", "La Dolce Vita", 8, "Drama", "Y");
            insert_record_UDT("04", "Saw", 21, "Thriller", "Y");
            insert_record_UDT("05", "The Mask", 47, "Comedy", "Y");
            insert_record_UDT("06", "Inception", 53, "Thriller", "Y");
            insert_record_UDT("07", "Arrival", 20, "Drama", "Y");
            insert_record_UDT("08", "Smallfoot", 5, "Comedy", "Y");
            insert_record_UDT("09", "Venom", 8, "Sci-Fi", "Y");
            insert_record_UDT("10", "Game Night", 2, "Comedy", "Y");
        }

        private void service_obj_Click(object sender, EventArgs e)
        {
            try
            {
                // company service
                SAPbobsCOM.CompanyService oCompanyService;
                oCompanyService = oCompany.GetCompanyService();

                // admin info
                SAPbobsCOM.AdminInfo oCompAdminInfo;
                oCompAdminInfo = oCompanyService.GetAdminInfo();

                // set the admin info to purple
                oCompAdminInfo.CompanyColor = 3;

                // update the setting of the admin info, see the effect
                oCompanyService.UpdateAdminInfo(oCompAdminInfo);

                MessageBox.Show("Background Color changed");
            }
            catch (Exception ex) {
                //MessageBox.Show("Exception: " + ex.Message);
                MessageBox.Show($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }
    }
}
