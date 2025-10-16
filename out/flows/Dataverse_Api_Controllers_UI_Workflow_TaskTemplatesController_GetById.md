[web] GET /api/ui/workflow/task-templates/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.GetById)  [L45–L53] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateDto [L48]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
  └─ calls TaskTemplateRepository.ReadQuery [L48]
  └─ query TaskTemplate [L48]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method ReadQuery [L48]
      └─ ... (no implementation details available)

