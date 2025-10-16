[web] DELETE /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.DeleteSubscription)  [L87–L100] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L99]
  └─ uses_service IMapper
    └─ method Map [L99]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L90]
      └─ ... (no implementation details available)

