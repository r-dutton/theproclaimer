[web] GET /workflow/v1/task-templates/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.FullQuery)  [L87–L95]
  └─ calls TaskTemplateRepository.ReadQuery [L91]
  └─ queries TaskTemplate [L91]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method ReadQuery [L91]

