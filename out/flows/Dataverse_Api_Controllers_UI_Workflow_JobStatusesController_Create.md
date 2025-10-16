[web] POST /api/ui/workflow/job-statuses  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.Create)  [L67–L89] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to JobStatusDto [L88]
  └─ calls JobStatusRepository.Add [L86]
  └─ calls JobStatusRepository.WriteQuery [L70]
  └─ calls KanbanColumnRepository.ReadQuery [L76]
  └─ insert JobStatus [L86]
    └─ reads_from DVS_JobStatuses
  └─ write JobStatus [L70]
    └─ reads_from DVS_JobStatuses
  └─ query KanbanColumn [L76]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<JobStatus>
    └─ method WriteQuery [L70]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L76]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L88]
      └─ ... (no implementation details available)

