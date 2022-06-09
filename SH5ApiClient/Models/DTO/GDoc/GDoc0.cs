namespace SH5ApiClient.Models.DTO
{
    public class GDoc0
    {
        public GDoc? Header { get; set; }


        public static GDoc0 Parse(ExecOperation answear)
        {

            ExecOperationContent headerContent = answear.GetAnswearContent("111");
            return new GDoc0
            {
                Header = GDoc.Parse(headerContent.GetValues()[0]),
            };
        }
    }
}
