docker build -f ./webapi.dockerfile -t apigatewayy:latest .
docker build -f ./user.dockerfile -t userapi:latest .


export AWS_PROFILE=dev

# 區域（你的 ECR 建在哪就設哪）
export AWS_REGION=ap-east-2

# 取帳號與 ECR Registry
ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
ECR="${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
echo "$ECR"  # 應該像 085980781371.dkr.ecr.ap-east-2.amazonaws.com

aws ecr get-login-password --region "$AWS_REGION" \
| docker login --username AWS --password-stdin "$ECR"

docker tag apigateway:1.0.0 "${ECR}/apigateway:1.0.0"
docker tag userapi:1.0.0    "${ECR}/userapi:1.0.0"

docker push "${ECR}/apigateway:1.0.0"
docker push "${ECR}/userapi:1.0.0"
