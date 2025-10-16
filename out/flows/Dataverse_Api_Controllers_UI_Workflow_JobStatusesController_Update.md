[web] PUT /api/ui/workflow/job-statuses/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.Update)  [L91–L101] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls JobStatusRepository.WriteQuery [L94]
  └─ write JobStatus [L94]
    └─ reads_from DVS_JobStatuses
  └─ uses_service IControlledRepository<JobStatus>
    └─ method WriteQuery [L94]
      └─ ... (no implementation details available)

