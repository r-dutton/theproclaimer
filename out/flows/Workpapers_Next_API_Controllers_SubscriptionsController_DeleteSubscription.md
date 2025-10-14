[web] DELETE /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.DeleteSubscription)  [L87–L100] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L99]
  └─ uses_service IMapper
    └─ method Map [L99]
  └─ uses_service UnitOfWork
    └─ method Table [L90]

