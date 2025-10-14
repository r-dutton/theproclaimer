[web] GET /api/internal/notification-subscriptions/record-templates  (Workpapers.Next.API.Controllers.Internal.NotificationSubscriptionController.GetUpdatedRecordTemplatesQuery)  [L26–L31] [auth=AuthorizationPolicies.M2M]
  └─ sends_request GetUpdatedRecordTemplateQuery [L29]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.NotificationSubscriptions.GetUpdatedRecordTemplateQueryHandler.Handle [L22–L62]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L33]

