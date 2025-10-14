[web] DELETE /api/ui/workflow/deliverables/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.Delete)  [L143–L156] [auth=Authentication.UserPolicy]
  └─ calls DeliverableRepository.WriteQuery [L146]
  └─ writes_to Deliverable [L146]
    └─ reads_from Deliverables
  └─ uses_service DeliverableRepository
    └─ method WriteQuery [L146]
  └─ sends_request CanIAccessJobQuery [L149]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIAccessJobQueryHandler.Handle [L39–L95]
      └─ uses_service IControlledRepository<Job>
        └─ method ReadQuery [L78]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L68]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L82]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L73]
      └─ uses_service UserService
        └─ method GetUserAsync [L71]
      └─ uses_cache IDistributedCache [L91]
        └─ method SetRecordAsync [write] [L91]
      └─ uses_cache IDistributedCache [L75]
        └─ method DoesRecordExistAsync [access] [L75]
      └─ uses_cache IDistributedCache [L73]
        └─ method CreateAccessKey [write] [L73]

