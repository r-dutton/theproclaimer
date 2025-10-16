[web] PUT /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Update)  [L63–L68] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobDto [L67]
    └─ converts_to JobExportDto [L312]
  └─ uses_service IMapper
    └─ method Map [L67]
      └─ ... (no implementation details available)
  └─ sends_request UpdateJobCommand [L66]

