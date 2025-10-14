[web] POST /api/ui/workflow/job-statuses  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.Create)  [L67–L89] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to JobStatusDto [L88]
  └─ calls JobStatusRepository.Add [L86]
  └─ calls JobStatusRepository.WriteQuery [L70]
  └─ calls KanbanColumnRepository.ReadQuery [L76]
  └─ writes_to JobStatus [L86]
    └─ reads_from DVS_JobStatuses
  └─ writes_to JobStatus [L70]
    └─ reads_from DVS_JobStatuses
  └─ queries KanbanColumn [L76]
    └─ reads_from KanbanColumns
  └─ uses_service IControlledRepository<JobStatus>
    └─ method WriteQuery [L70]
  └─ uses_service IControlledRepository<KanbanColumn>
    └─ method ReadQuery [L76]
  └─ uses_service IMapper
    └─ method Map [L88]

