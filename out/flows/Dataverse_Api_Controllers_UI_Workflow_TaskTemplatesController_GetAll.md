[web] GET /api/ui/workflow/task-templates  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.GetAll)  [L33–L43] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaskTemplateDto [L36]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
  └─ calls TaskTemplateRepository.ReadQuery [L36]
  └─ query TaskTemplate [L36]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplate writes=0 reads=1
    └─ mappings 1
      └─ TaskTemplateDto

