[web] PUT /api/internal/jobs/{id:guid}  (Dataverse.Api.Controllers.Internal.Workflow.JobsController.Update)  [L63–L68] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobDto [L67]
    └─ converts_to JobExportDto [L312]
  └─ sends_request UpdateJobCommand [L66]
  └─ impact_summary
    └─ requests 1
      └─ UpdateJobCommand
    └─ mappings 1
      └─ JobDto

