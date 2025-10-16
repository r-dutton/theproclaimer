[web] POST /api/ui/workflow/job-statuses  (Dataverse.Api.Controllers.UI.Workflow.JobStatusesController.Create)  [L67–L89] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to JobStatusDto [L88]
  └─ calls JobStatusRepository.Add [L86]
  └─ calls KanbanColumnRepository.ReadQuery [L76]
  └─ calls JobStatusRepository.WriteQuery [L70]
  └─ insert JobStatus [L86]
    └─ reads_from DVS_JobStatuses
  └─ query KanbanColumn [L76]
    └─ reads_from KanbanColumns
  └─ write JobStatus [L70]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 2 (writes=2, reads=1)
      └─ JobStatus writes=2 reads=0
      └─ KanbanColumn writes=0 reads=1
    └─ mappings 1
      └─ JobStatusDto

