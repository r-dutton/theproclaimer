[web] PUT /api/internal/task-templates/{id:Guid}/reorder  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Reorder)  [L83–L99] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskTemplateRepository.WriteQuery [L86]
  └─ writes_to TaskTemplate [L86]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L86]

