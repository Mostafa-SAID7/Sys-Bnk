using System;
using System.Collections.Generic;

public class User
{
    public string CardNumber { get; set; }
    public string Pin { get; set; }
    public decimal Balance { get; set; }
    public List<string> TransactionHistory { get; set; } = new();
    public int FailedAttempts { get; set; } = 0;
    public DateTime? LockoutTime { get; set; } = null;

    public bool IsLocked()
    {
        if (FailedAttempts < 3) return false;
        if (LockoutTime == null) return true;

        if ((DateTime.Now - LockoutTime.Value).TotalMinutes >= 5)
        {
            FailedAttempts = 0;
            LockoutTime = null;
            return false;
        }

        return true;
    }

    public TimeSpan? TimeUntilUnlock()
    {
        if (LockoutTime.HasValue)
        {
            var unlockAt = LockoutTime.Value.AddMinutes(5);
            return unlockAt > DateTime.Now ? unlockAt - DateTime.Now : null;
        }

        return null;
    }
}