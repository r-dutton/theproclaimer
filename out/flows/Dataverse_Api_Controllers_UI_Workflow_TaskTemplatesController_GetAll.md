[web] GET /api/ui/workflow/task-templates  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.GetAll)  [L33–L43] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateDto [L36]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
  └─ calls TaskTemplateRepository.ReadQuery [L36]
  └─ query TaskTemplate [L36]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method ReadQuery [L36]
      └─ ... (no implementation details available)

