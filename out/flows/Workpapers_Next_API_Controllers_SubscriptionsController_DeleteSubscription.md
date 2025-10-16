[web] DELETE /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.DeleteSubscription)  [L87–L100] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L99]
  └─ uses_service UnitOfWork
    └─ method Table [L90]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SubscriptionDto

