[web] POST /api/ui/workflow/task-templates  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Create)  [L55–L69] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to TaskTemplateDto [L68]
  └─ calls TaskTemplateRepository.Add [L66]
  └─ calls TaskTemplateRepository.WriteQuery [L58]
  └─ writes_to TaskTemplate [L66]
    └─ reads_from TaskTemplates
  └─ writes_to TaskTemplate [L58]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L58]
  └─ uses_service IMapper
    └─ method Map [L68]

