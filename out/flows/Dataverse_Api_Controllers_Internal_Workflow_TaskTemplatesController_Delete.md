[web] DELETE /api/internal/task-templates/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Delete)  [L101–L111] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskTemplateRepository.Remove [L108]
  └─ calls TaskTemplateRepository.WriteQuery [L104]
  └─ writes_to TaskTemplate [L108]
    └─ reads_from TaskTemplates
  └─ writes_to TaskTemplate [L104]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L104]

