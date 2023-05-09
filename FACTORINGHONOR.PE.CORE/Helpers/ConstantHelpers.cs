using System;
using System.Collections.Generic;
using System.Text;

namespace FACTORINGHONOR.PE.CORE.Helpers
{
    public static class ConstantHelpers
    {
        public static class Seed
        {
            public const bool ENABLED = false;
        }

        public static class Project
        {
            public const string TITLE = "Factoring Honor";
            public const string DESCRIPTION = "Sistema Web para la Gestión de Carteras de Descuentos de Recibos por Honorarios";
        }

        public static class TimeZone
        {
            public const string DEFAULT_ID = "SA Pacific Standard Time";
        }

        public static class Format
        {
            public const string DATE = "dd/MM/yyyy";
            public const string DURATION = "{0}h {1}m";
            public const string TIME = "h:mm tt";
            public const string DATETIME = "dd/MM/yyyy h:mm tt";
        }

        public static class Rate
        {
            public static class TermDays
            {
                public const int YEARLY = Bank.COMMERCIAL_YEAR;
                public const int BIYEARLY = 180;
                public const int QUARTERLY = 90;
                public const int BIMONTHLY = 60;
                public const int MONTHLY = 30;
                public const int BIWEEKLY = 15;
                public const int WEEKLY = 7;
                public const int DAILY = 1;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                {
                    { YEARLY, "Anual" },
                    { BIYEARLY, "Semestral" },
                    { QUARTERLY, "Trimestral" },
                    { BIMONTHLY, "Bimestral" },
                    { MONTHLY, "Mensual" },
                    { BIWEEKLY, "Quincenal" },
                    { WEEKLY, "Semanal" },
                    { DAILY, "Diario" },
                };
            }

            public static class Type
            {
                public const int EFFECTIVE = 0;
                public const int NOMINAL = 1;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                {
                    { EFFECTIVE, "Efectiva" },
                    { NOMINAL, "Nominal" }
                };
            }
        }

        public static class Bank
        {
            public const int COMMERCIAL_YEAR = 360;
        }

        public static class Role
        {
            public const string ADMIN = "Admin";
            public const string CUSTOMER = "Cliente";
        }

        public static class MESSAGES
        {
            public static class ERROR
            {
                public const string MESSAGE = "Ocurrió un problema al procesar su consulta";
                public const string TITLE = "Error";
            }

            public static class INFO
            {
                public const string MESSAGE = "Mensaje informativo";
                public const string TITLE = "Info";
            }

            public static class SUCCESS
            {
                public const string MESSAGE = "Tarea realizada satisfactoriamente";
                public const string TITLE = "Éxito";
            }

            public static class VALIDATION
            {
                public const string COMPARE = "El campo '{0}' no coincide con '{1}'";
                public const string EMAIL_ADDRESS = "El campo '{0}' no es un correo electrónico válido";
                public const string RANGE = "El campo '{0}' debe tener un valor entre {1}-{2}";
                public const string REGULAR_EXPRESSION = "El campo '{0}' no es válido";
                public const string REQUIRED = "El campo '{0}' es obligatorio";
                public const string STRING_LENGTH = "El campo '{0}' debe tener {1}-{2} caracteres";
                public const string NOT_VALID = "El campo '{0}' no es válido'";
                public const string FILE_EXTENSIONS = "El campo '{0}' solo acepta archivos con los formatos: {1}";
            }
        }

        public static class Url
        {
            public const string RUC_CHECK = "https://www.wmtechnology.org/Consultar-RUC/";
        }
    }
}
