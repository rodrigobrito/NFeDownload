using System;

namespace NFeDownload.NFe.SchemaXML
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ClasseServico : Attribute
    {
        String _value;

        public String value
        {
            get { return _value; }
            set { _value = value; }
        }
    }


    [AttributeUsage(AttributeTargets.Field)]
    public class AtendidoPor : Attribute
    {
        String _value;

        public String value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SVC_AtendidoPor : Attribute
    {
        String _value;

        public String value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
