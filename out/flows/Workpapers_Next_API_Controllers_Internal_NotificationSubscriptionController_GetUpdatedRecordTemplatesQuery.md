[web] GET /api/internal/notification-subscriptions/record-templates  (Workpapers.Next.API.Controllers.Internal.NotificationSubscriptionController.GetUpdatedRecordTemplatesQuery)  [L26–L31] status=200 [auth=AuthorizationPolicies.M2M]
  └─ sends_request GetUpdatedRecordTemplateQuery -> GetUpdatedRecordTemplateQueryHandler [L29]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.NotificationSubscriptions.GetUpdatedRecordTemplateQueryHandler.Handle [L22–L62]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L33]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetUpdatedRecordTemplateQuery
    └─ handlers 1
      └─ GetUpdatedRecordTemplateQueryHandler

