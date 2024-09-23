using Domain.Entities;

namespace Application.Service;

public class RateLimitService
{
    private readonly List<RateLimitRule> _rateLimitRules;

    public RateLimitService()
    {
        _rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule("status", 2, TimeSpan.FromMinutes(1)),
                new RateLimitRule("news", 1, TimeSpan.FromDays(1)),
                new RateLimitRule("marketing", 3, TimeSpan.FromHours(1))
            };
    }

    public RateLimitRule GetRateLimitRule(string notificationType)
    {
        return _rateLimitRules.FirstOrDefault(r => r.NotificationType == notificationType);
    }
}
