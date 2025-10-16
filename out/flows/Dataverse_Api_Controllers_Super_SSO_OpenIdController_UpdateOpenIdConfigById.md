[web] PUT /api/super/tenants/{tenantId}/sso/open-id/configs/{configId}  (Dataverse.Api.Controllers.Super.SSO.OpenIdController.UpdateOpenIdConfigById)  [L45–L50] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IOpenIdService (AddScoped)
    └─ method UpdateOpenIdConfigByIdAsync [L48]
      └─ implementation Dataverse.Services.Features.SSO.OpenIdService.UpdateOpenIdConfigByIdAsync [L9-L53]

