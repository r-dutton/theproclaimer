[web] DELETE /api/internal/task-templates/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Delete)  [L101–L111] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskTemplateRepository.Remove [L108]
  └─ calls TaskTemplateRepository.WriteQuery [L104]
  └─ delete TaskTemplate [L108]
    └─ reads_from TaskTemplates
  └─ write TaskTemplate [L104]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L104]
      └─ ... (no implementation details available)

