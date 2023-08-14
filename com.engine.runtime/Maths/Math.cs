namespace HCEngine.Math
{
    public static class Math
    {
        public static float ResetAngle(float angle)
        {
            return (180 <= angle) ? angle - 360 : angle;
        }
    }
}