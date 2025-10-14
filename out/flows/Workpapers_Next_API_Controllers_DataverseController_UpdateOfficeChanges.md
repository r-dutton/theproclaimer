[web] PUT /api/dataverse/offices  (Workpapers.Next.API.Controllers.DataverseController.UpdateOfficeChanges)  [L104–L110] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L108]
  └─ sends_request ProcessDataverseOfficeCommand [L109]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseOfficeCommandHandler.Handle [L23–L64]
      └─ uses_service IControlledRepository<Office>
        └─ method WriteQuery [L37]

