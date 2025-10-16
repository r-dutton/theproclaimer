[web] GET /api/super/tenants/{tenantId}/sso/open-id/configs/  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.GetAllOpenIdConfig)  [L31–L36] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IOpenIdService (AddScoped)
    └─ method GetAllOpenIdConfigAsync [L34]
      └─ implementation Dataverse.Services.Features.SSO.OpenIdService.GetAllOpenIdConfigAsync [L9-L53]

