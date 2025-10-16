[web] PUT /api/dataverse/offices  (Workpapers.Next.API.Controllers.DataverseController.UpdateOfficeChanges)  [L104–L110] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L108]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ sends_request ProcessDataverseOfficeCommand [L109]
    └─ handled_by Cirrus.Central.Dataverse.Commands.ProcessDataverseOfficeCommandHandler.Handle [L23–L64]
      └─ uses_service IControlledRepository<Office>
        └─ method WriteQuery [L37]
          └─ ... (no implementation details available)

