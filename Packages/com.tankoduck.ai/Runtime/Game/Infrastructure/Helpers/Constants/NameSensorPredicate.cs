using GOAP;

namespace Game.Infrastructure.Helpers.Constants
{
    public static class NameSensorPredicate
    {
        public static string HitSensorPredicate => ESensorType.HitSensor.ToString();
        public static string AttackSensorPredicate => ESensorType.AttackSensor.ToString();
        public static string ReceiveDamageSensorPredicate => ESensorType.ReceiveDamageSensor.ToString();
        public static string EyesSensorPredicate => ESensorType.EyesSensor.ToString();
    }
}