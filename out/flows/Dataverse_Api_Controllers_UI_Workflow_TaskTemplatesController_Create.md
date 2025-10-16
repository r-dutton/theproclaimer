[web] POST /api/ui/workflow/task-templates  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Create)  [L55–L69] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to TaskTemplateDto [L68]
  └─ calls TaskTemplateRepository.Add [L66]
  └─ calls TaskTemplateRepository.WriteQuery [L58]
  └─ insert TaskTemplate [L66]
    └─ reads_from TaskTemplates
  └─ write TaskTemplate [L58]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method WriteQuery [L58]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L68]
      └─ ... (no implementation details available)

