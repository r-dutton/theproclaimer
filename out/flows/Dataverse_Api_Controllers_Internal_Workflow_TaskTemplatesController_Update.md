[web] PUT /api/internal/task-templates/{id:Guid}  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Update)  [L71–L81] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TaskTemplateRepository.WriteQuery [L74]
  └─ write TaskTemplate [L74]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L74]
      └─ ... (no implementation details available)

