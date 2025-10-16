[web] GET /api/internal/task-templates  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Get)  [L33–L43] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TaskTemplateDto [L36]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
  └─ calls TaskTemplateRepository.ReadQuery [L36]
  └─ query TaskTemplate [L36]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method ReadQuery [L36]
      └─ ... (no implementation details available)

