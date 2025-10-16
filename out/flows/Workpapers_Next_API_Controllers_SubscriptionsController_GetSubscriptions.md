[web] GET /api/subscriptions/byfirm/{id:Guid}  (Workpapers.Next.API.Controllers.SubscriptionsController.GetSubscriptions)  [L50–L59] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L53]
  └─ uses_service UnitOfWork
    └─ method Table [L53]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SubscriptionDto

