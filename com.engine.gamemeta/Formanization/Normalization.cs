namespace HCEngine.Normal
{
    public static class Formanization
    {
        public static string NormalizeScore(float Score, string format = "0.0")
        {
            float trillion = 1000000000000;
            if (trillion <= Score)
                return string.Format("{0:" + format + "}", Score / trillion) + "T";

            float billion = 1000000000;
            if (billion <= Score)
                return string.Format("{0:" + format + "}", Score / billion) + "B";

            float million = 1000000;
            if (million <= Score)
                return string.Format("{0:" + format + "}", Score / million) + "M";

            float thousand = 1000;
            if (thousand <= Score)
                return string.Format("{0:" + format + "}", Score / thousand) + "K";


            return string.Format("{0:0}", Score);
        }
    }
}
