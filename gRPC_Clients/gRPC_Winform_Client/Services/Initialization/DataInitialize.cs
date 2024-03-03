using System;

namespace gRPC_Winform_Client.Services.Initialization
{
    public class DataInitialize
    {
        private readonly Form1 _form1;

        public DataInitialize(Form1 form1)
        {
            _form1 = form1;
            
            
        }


        
        public bool setup()
        {
            return true;



        }
    }
}