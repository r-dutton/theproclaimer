[web] PUT /api/dataverse/job-types  (Workpapers.Next.API.Controllers.DataverseController.UpdateJobTypeChanges)  [L137–L143] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ uses_service TenantService
    └─ method GetCurrentTenant [L141]
  └─ sends_request ProcessDataverseJobTypeCommand [L142]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.Dataverse.ProcessDataverseJobTypeCommandHandler.Handle [L21–L57]
      └─ uses_service IControlledRepository<JobType>
        └─ method WriteQuery [L34]

