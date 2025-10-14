[web] PUT /api/ui/workflow/jobs/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.JobController.UpdateJob)  [L177–L188] [auth=Authentication.UserPolicy]
  └─ maps_to JobLightDto [L184]
  └─ uses_service IMapper
    └─ method Map [L184]
  └─ sends_request UpdateJobCommand [L180]

