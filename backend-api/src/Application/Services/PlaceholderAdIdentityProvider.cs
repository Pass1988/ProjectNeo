using BackendApi.Application.Interfaces;

namespace BackendApi.Application.Services;

public class PlaceholderAdIdentityProvider : IAdIdentityProvider
{
    public bool IsEnabled => false;
    public string Description => "Active Directory non ancora implementato (placeholder).";
}
