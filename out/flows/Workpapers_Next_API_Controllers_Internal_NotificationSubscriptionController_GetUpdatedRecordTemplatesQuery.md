[web] GET /api/internal/notification-subscriptions/record-templates  (Workpapers.Next.API.Controllers.Internal.NotificationSubscriptionController.GetUpdatedRecordTemplatesQuery)  [L26–L31] status=200 [auth=AuthorizationPolicies.M2M]
  └─ sends_request GetUpdatedRecordTemplateQuery [L29]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.NotificationSubscriptions.GetUpdatedRecordTemplateQueryHandler.Handle [L22–L62]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L33]
          └─ ... (no implementation details available)

