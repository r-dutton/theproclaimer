[web] GET /workflow/v1/task-templates/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.Get)  [L49–L55]
  └─ maps_to TaskTemplateDto [L52]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L375]
  └─ calls TaskTemplateRepository.ReadQuery [L52]
  └─ queries TaskTemplate [L52]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method ReadQuery [L52]

