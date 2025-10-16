[web] GET /api/super/workflow/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workflow.WorkflowsSubscriptionController.GetSubscription)  [L42–L55] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L47]
  └─ calls TenantRepository.ReadTable [L47]
  └─ query Tenant [L47]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L47]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

