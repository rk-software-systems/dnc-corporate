namespace DNCCorporate.Services;

public class ApplicationDateService : IApplicationDateService
{
    #region props 

    public DateTime StartedOnUtc { get; }

    #endregion

    #region ctors

    public ApplicationDateService()
    {
        StartedOnUtc = DateTime.UtcNow.Date;
    }
    #endregion
}
