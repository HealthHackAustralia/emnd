from rest_framework import status
from rest_framework.decorators import api_view, permission_classes
from rest_framework.response import Response
from medical_form.models import Submission
from medical_form.serializers import SubmissionSerializer
from rest_framework.permissions import IsAdminUser

@api_view(['POST'])
def submission_post(request):
    """
    Create a new submission.
    """
    if request.method == 'POST':
        serializer = SubmissionSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            # print(serializer.data['user_id'])
            # userId = serializer.data['user_id']
            # submissions = Submission.objects.filter(user_id=userId).reverse()
            # if len(submissions) > 1:
            #     print(submissions[0].__dict__)
            #     print(submissions[1].__dict__)
            # else:

            # print(serializer.data)
            # print(serializer.data['d21a'])
            # Data analytics code
            # return output
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


@api_view(['GET'])
@permission_classes((IsAdminUser, ))
def submission_list(request):
    """
    List all submissions.
    """
    if request.method == 'GET':
        submissions = Submission.objects.all()
        serializer = SubmissionSerializer(submissions, many=True)
        return Response(serializer.data)



@api_view(['GET', 'PUT'])
@permission_classes((IsAdminUser, ))
def submission_detail(request, pk):
    """
    Retrieve, update or delete a code snippet.
    """
    try:
        submission = Submission.objects.get(pk=pk)
    except Submission.DoesNotExist:
        return Response(status=status.HTTP_404_NOT_FOUND)

    if request.method == 'GET':
        serializer = SubmissionSerializer(submission)
        return Response(serializer.data)

    elif request.method == 'PUT':
        serializer = SubmissionSerializer(submission, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    elif request.method == 'DELETE':
        submission.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)