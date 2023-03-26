namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
        public IEnumerable<WeatherForecast> Post(int count, int min, int max);

    }
}