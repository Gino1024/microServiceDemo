# Makefile

AWS_REGION ?= ap-east-2
ACCOUNT_ID := $(shell aws sts get-caller-identity --query Account --output text)
ECR        := $(ACCOUNT_ID).dkr.ecr.$(AWS_REGION).amazonaws.com
VERSION    ?= 1.0.0   # 預設版號，可以透過 make VERSION=xxx 覆寫

# 預設目標：建置兩個 image
all: apigateway userapi

# --- Build ---
apigateway:
	docker build -f ./webapi.dockerfile -t apigateway:$(VERSION) .

userapi:
	docker build -f ./user.dockerfile -t userapi:$(VERSION) .

# --- Push to ECR ---  手動時使用而已, 需手動調整版號, 後續藉由Github Action
push: all
	aws ecr get-login-password --region $(AWS_REGION) \
	| docker login --username AWS --password-stdin $(ECR)
	docker tag apigateway:$(VERSION) $(ECR)/apigateway:$(VERSION)
	docker tag userapi:$(VERSION)    $(ECR)/userapi:$(VERSION)
	docker push $(ECR)/apigateway:$(VERSION)
	docker push $(ECR)/userapi:$(VERSION)

# --- Clean ---
clean:
	docker rmi apigateway:$(VERSION) userapi:$(VERSION) || true
	docker rmi $(ECR)/apigateway:$(VERSION) $(ECR)/userapi:$(VERSION) || true
