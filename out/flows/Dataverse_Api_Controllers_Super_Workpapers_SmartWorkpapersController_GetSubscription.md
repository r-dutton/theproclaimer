[web] GET /api/super/smart-workpapers/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.GetSubscription)  [L63–L78] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L68]
  └─ uses_client WorkpapersClient [L75]
  └─ calls TenantRepository.ReadTable [L68]
  └─ query Tenant [L68]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L68]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

