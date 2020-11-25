class ApprovedOrderCannotBeRejectedError(Exception):
    def __repr__(self):
        return "ApprovedOrderCannotBeRejectedError"
