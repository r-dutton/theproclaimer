[web] PUT /api/dataverse/job-types  (Workpapers.Next.API.Controllers.DataverseController.UpdateJobTypeChanges)  [L137–L143] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L141]
      └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
  └─ sends_request ProcessDataverseJobTypeCommand [L142]
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ProcessDataverseJobTypeCommandHandler.Handle [L21–L57]
      └─ uses_service IControlledRepository<JobType>
        └─ method WriteQuery [L34]
          └─ ... (no implementation details available)

