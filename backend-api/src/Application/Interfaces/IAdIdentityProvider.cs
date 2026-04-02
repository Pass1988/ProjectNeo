namespace BackendApi.Application.Interfaces;

public interface IAdIdentityProvider
{
    bool IsEnabled { get; }
    string Description { get; }
}
