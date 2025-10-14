[web] PUT /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Update)  [L63–L68] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobDto [L67]
    └─ converts_to JobExportDto [L311]
  └─ uses_service IMapper
    └─ method Map [L67]
  └─ sends_request UpdateJobCommand [L66]

