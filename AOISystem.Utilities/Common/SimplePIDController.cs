
namespace AOISystem.Utilities.Common
{
    public class SimplePIDController
    {        
        public double SetPoint { get; set; }

        public double Kp { get; set; }

        public double Ki { get; set; }

        public double Kd { get; set; }

        public double Error { get; set; }

        public double PreError { get; set; }

        public double Integral { get; set; }

        public double Derivative { get; set; }

        public SimplePIDController(double setPoint, double kp, double ki, double kd)
        {
            this.SetPoint = setPoint;
            this.Kp = kp;
            this.Ki = ki;
            this.Kd = kd;
            this.Error = 0;
            this.PreError = 0;
            this.Integral = 0;
            this.Derivative = 0;
        }

        public override string ToString()
        {
            return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", this.SetPoint, this.Kp, this.Ki, this.Kd, this.Error, this.PreError, this.Integral, this.Derivative);
        }

        public bool CompareParamter(double setPoint, double kp, double ki, double kd)
        {
            return (setPoint == this.SetPoint) && (kp == this.Kp) && (ki == this.Ki) && (kd == this.Kd);
        }

        public double Update(double sensor, double dt)
        {
            this.Error = this.SetPoint - sensor;
            this.Integral = this.Integral + (this.Error * dt);
            this.Derivative = (this.Error - this.PreError) / dt;
            this.PreError = this.Error;

            return (this.Kp * this.Error) + (this.Ki * this.Integral) + (this.Kd * this.Derivative);
        }
    }
}
