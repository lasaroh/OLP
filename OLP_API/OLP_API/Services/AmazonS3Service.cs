using Amazon;
using Amazon.S3;
using Amazon.Runtime;
using Amazon.S3.Model;

namespace OLP_API.Services
{

	public class AmazonS3Service
	{
		private readonly AWSCredentials Credentials;
		private readonly AmazonS3Client s3Client;
		private const string bucketName = "bucket-lasaroh-olp";
		private const string AccessKeysID = "AKIAYPZF2NGEPOIXXU6G";
		private const string SecretAccessKey = "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";

		public AmazonS3Service()
		{
			Credentials = new BasicAWSCredentials(AccessKeysID, SecretAccessKey);
			s3Client = new AmazonS3Client(Credentials, RegionEndpoint.EUWest3);
		}

		public string GetLesson(int IdCourse, int OrderLesson)
		{
			string objectKey = "Course" + IdCourse.ToString() + "/Lesson" + OrderLesson.ToString() + ".mp4";
			var request = new GetPreSignedUrlRequest
			{
				BucketName = bucketName,
				Key = objectKey,
				Expires = DateTime.UtcNow.AddMinutes(30)
			};
			return s3Client.GetPreSignedURL(request);
		}
	}
}
